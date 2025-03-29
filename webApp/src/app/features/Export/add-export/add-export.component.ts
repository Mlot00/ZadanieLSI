import { Component, OnInit } from '@angular/core';
import { ExportService } from '../service/export.service';
import { ExportRequestDto } from '../models/export-request-dto';
import { Location } from '@angular/common';
import { LOCATION_OPTIONS } from '../../../shared/location-options';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-export',
  standalone: false,
  templateUrl: './add-export.component.html',
  styleUrl: './add-export.component.scss'
})
export class AddExportComponent implements OnInit {

  export: ExportRequestDto = {
    exportName: '',
    exportDateTime: new Date(),
    username: '',
    location: ''
  };

  exportDate: Date | null = null;
  exportTime: string = '';

  locationOptions = LOCATION_OPTIONS;

  constructor(
    private exportService: ExportService,
    private location: Location,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
  }

  onSubmit(): void {

    if (this.exportDate && this.exportTime) {
      const [hours, minutes] = this.exportTime.split(':').map(Number);
      const combinedDateTime = new Date(this.exportDate);
      combinedDateTime.setHours(hours, minutes, 0, 0);
      this.export.exportDateTime = combinedDateTime;
    }

    this.exportService.createExport(this.export).subscribe({
      next: (createdExport) => {
        console.log('Export został utworzony:', createdExport);
        this.snackBar.open('Export został utworzony pomyślnie', 'Zamknij', {
          duration: 3000,
          verticalPosition: "top"
        });
        this.location.back();
      },
      error: (error) => {
        this.snackBar.open('Niepowodzenie', 'Zamknij', {
          duration: 3000,
          verticalPosition: "top",
        });
      }
    });
  }

}


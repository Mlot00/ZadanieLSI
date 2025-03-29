import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ExportResponseDto } from '../models/export-response-dto';
import { ExportFilterRequestDto } from '../models/export-filter-request-dto';
import { ExportService } from '../service/export.service';
import { PagedResult } from '../models/paged-result';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { LOCATION_OPTIONS } from '../../../shared/location-options';

@Component({
  selector: 'app-export-list',
  standalone: false,
  templateUrl: './export-list.component.html',
  styleUrl: './export-list.component.scss'
})
export class ExportListComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['exportName', 'exportDate', 'exportTime', 'username', 'location'];
  dataSource = new MatTableDataSource<ExportResponseDto>([]);
  totalCount?: number;

  filter: ExportFilterRequestDto = {
    pageNumber: 1,
    pageSize: 5,
    fromDate: null,
    toDate: null,
    location: null
  };

  locationOptions = LOCATION_OPTIONS;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private exportService: ExportService) { }

  ngOnInit(): void {
    this.searchExports();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    console.log('Paginator przypisany:', this.paginator);
    this.dataSource.sortingDataAccessor = (data, sortHeaderId) => {
      if (sortHeaderId === 'exportDate' || sortHeaderId === 'exportTime') {
        return new Date(data.exportDateTime).getTime();
      }
      return (data as any)[sortHeaderId];
    };
  }

  searchExports(): void {
    console.log(this.filter);
    this.exportService.getExports(this.filter).subscribe({
      next: (result: PagedResult<ExportResponseDto>) => {
        this.totalCount = result.totalCount;
        console.log('Total Count: ',this.totalCount);
        this.dataSource.data = result.items;
      },
      error: err => console.error('Błąd podczas pobierania eksportów:', err)
    });
  }

  onPageChange(event: PageEvent): void {
    this.filter.pageNumber = event.pageIndex + 1;
    this.filter.pageSize = event.pageSize;
    console.log('Nowa strona:', this.filter.pageNumber, 'PageSize:', this.filter.pageSize);
    this.searchExports();
  }

}
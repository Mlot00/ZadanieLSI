import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExportResponseDto } from '../models/export-response-dto';
import { PagedResult } from '../models/paged-result';
import { Observable } from 'rxjs';
import { ExportFilterRequestDto } from '../models/export-filter-request-dto';
import { environment } from '../../../../environments/environment';
import { ExportRequestDto } from '../models/export-request-dto';

@Injectable({
  providedIn: 'root'
})
export class ExportService {
  private controllerUrl = 'exports'; 

  constructor(private http: HttpClient) {}

    getExports(filter: ExportFilterRequestDto): Observable<PagedResult<ExportResponseDto>> {
      let params = new HttpParams();
  
      if (filter.fromDate) {
        params = params.set('fromDate', filter.fromDate.toISOString());
      }
      if (filter.toDate) {
        params = params.set('toDate', filter.toDate.toISOString());
      }
      if (filter.location) {
        params = params.set('location', filter.location);
      }
      params = params.set('pageNumber', filter.pageNumber.toString());
      params = params.set('pageSize', filter.pageSize.toString());
  
      return this.http.get<PagedResult<ExportResponseDto>>(`${environment.apiBaseUrl}/${this.controllerUrl}`, { params });
    }
  
    createExport(exportRequest: ExportRequestDto): Observable<ExportResponseDto> {
      return this.http.post<ExportResponseDto>(`${environment.apiBaseUrl}/${this.controllerUrl}`, exportRequest);
    }
}

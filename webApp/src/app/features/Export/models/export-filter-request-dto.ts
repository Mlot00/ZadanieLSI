export interface ExportFilterRequestDto {
    fromDate?: Date | null; 
    toDate?: Date | null;   
    location?: string | null; 
    pageNumber: number;
    pageSize: number;
  }
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { ReportStatus } from '../model/enums.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  apiUrl: string = 'http://localhost:5234/api/'
  constructor(private http: HttpClient) { }

  updateReports(id: string, newStatus: ReportStatus): Observable<boolean> {
   
    return this.http.patch<boolean>(`${this.apiUrl}reports/${id}?status=${newStatus}`, {}).pipe(
      map((response) => {     
        return response;
      })
      ,
      catchError((error) => {
        const statusCode = error.status;
        alert(error.error);
        return of(false); // Return an empty array on error
      })
    );
  }
}

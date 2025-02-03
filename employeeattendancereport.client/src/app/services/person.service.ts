import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Person } from '../model/person.model';
import { catchError, map, Observable, of } from 'rxjs';
import { Manager } from '../model/manager.model';
import { PersonReport } from '../model/personReport.model';
import { Report } from '../model/report.model';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  apiUrl: string = 'http://localhost:5234/api/'
  constructor(private http: HttpClient) { }

  getAllPersons(): Observable<Person[]>{
    return this.http.get<Person[]>(`${this.apiUrl}persons/all`).pipe(
      map((response) => {   
        return response;
      })
      ,
      catchError((error) => {
        const statusCode = error.status;       
        return of([]); // Return an empty array on error
      })
    );
  }

  getManager(id: number): Observable<Manager | undefined> {
    return this.http.get<Manager>(`${this.apiUrl}persons/${id}/manager`).pipe(
      map((response) => {  
        return response;
      })
      ,
      catchError((error) => {  
        const statusCode = error.status;      
        return of(undefined); // Return an empty array on error
      })
    );
  }

  getPersonReports(id: number): Observable<PersonReport[]>{
    return this.http.get<PersonReport[]>(`${this.apiUrl}persons/${id}/reports`).pipe(
      map((response) => {
        return response;
      })
      ,
      catchError((error) => {
        const statusCode = error.status;        
        alert(error.error);        
        return of([]); // Return an empty array on error
      })
    );
  }


  createPersonReport(id: number, _data: Report): Observable<any> {
    return this.http.post(`${this.apiUrl}persons/${id}/report`, _data).pipe(
      map((response) => {     
        return response;
      }),
      catchError((error) => {        
        const statusCode = error.status;

        if (statusCode === 401 || statusCode === 400 || statusCode === 409) {
          alert(error.error);
        } else if (statusCode === 500) {
          alert("Internal Server Error");
        }
        return of(null);
      })
    );
  }


}

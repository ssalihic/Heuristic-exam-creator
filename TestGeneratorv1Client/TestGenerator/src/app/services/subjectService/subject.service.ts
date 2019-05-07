import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, retry} from 'rxjs/operators';
import {throwError} from 'rxjs/internal/observable/throwError';
import {ApiService} from '../apiService/api.service';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  error;
  constructor(private httpClient: HttpClient, private apiService: ApiService) {}
  get() {
    return this.httpClient.get(this.apiService.baseUrl + 'subject').pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }

  getId(id) {
    return this.httpClient.get(this.apiService.baseUrl + 'subject/' + id).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }

  getYearOfStudies(id: number) {
    return this.httpClient.get(this.apiService.baseUrl + 'subject/yearOfStudies/' + id).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }
  getAreas(idSubject: number, idArea: number) {
    return this.httpClient.get(this.apiService.baseUrl + 'subject/areas/' + idSubject + '/' + idArea).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }
  add(subject) {
    return this.httpClient.post(this.apiService.baseUrl + 'subject', subject).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }
  update(id: number, subject) {
    return this.httpClient.put(this.apiService.baseUrl + 'subject/' + id, subject).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    ).subscribe();
  }


}

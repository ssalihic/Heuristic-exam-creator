import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import { ApiService } from '../apiService/api.service';
import {catchError, retry} from 'rxjs/operators';
import {throwError} from 'rxjs/internal/observable/throwError';
import {Http, Headers, RequestOptions} from '@angular/http';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  constructor(private httpClient: HttpClient, private apiService: ApiService) { }
  generateTest (test) {
    return this.httpClient.post(this.apiService.baseUrl + 'test/generateTest', test).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );

  }
  addTest (test) {
    return this.httpClient.post(this.apiService.baseUrl + 'test', test).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );

  }
  get () {
    return this.httpClient.get(this.apiService.baseUrl + 'test').pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }
  validate(test) {
    return this.httpClient.post(this.apiService.baseUrl + 'test/testValidation', test).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }
  recent(id) {

    return this.httpClient.post(this.apiService.baseUrl + 'test/recentTests/' + id, id).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );



  }


  getAll(indexi: number, limit: number, skip: number, id: string) {
    return this.httpClient.post(this.apiService.baseUrl + 'test/pagination/limit=' + limit + '&skip=' + skip + '&id=' + id, indexi).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }
}


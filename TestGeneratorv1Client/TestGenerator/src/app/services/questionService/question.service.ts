import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {Body} from '@angular/http/src/body';
import {catchError, retry} from 'rxjs/operators';
import {throwError} from 'rxjs/internal/observable/throwError';
import {ApiService} from '../apiService/api.service';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  constructor(private httpClient: HttpClient, private apiService: ApiService ) {}
  get() {
    return this.httpClient.get(this.apiService.baseUrl + 'question').pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }

  getAll(indexi: Array<number>, limit: number, skip: number, id: String) {
    return this.httpClient.post(this.apiService.baseUrl + 'question/pagination/limit=' + limit + '&skip=' + skip + '&id=' + id, indexi).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }

  addQuestion (question) {
    this.httpClient.post(this.apiService.baseUrl + 'question', question).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    ).subscribe();
  }
  editQuestion (id, question) {
    this.httpClient.put(this.apiService.baseUrl + 'question/' + id, question).pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    ).subscribe();
  }

}

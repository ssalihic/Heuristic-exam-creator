import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, retry} from 'rxjs/operators';
import {throwError} from 'rxjs/internal/observable/throwError';
import {ApiService} from '../apiService/api.service';

@Injectable({
  providedIn: 'root'
})
export class DifficultyLevelService {

  constructor(private httpClient: HttpClient, private apiService: ApiService ) {}
  get() {
    return this.httpClient.get(this.apiService.baseUrl + 'difficultyLevel').pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }

}

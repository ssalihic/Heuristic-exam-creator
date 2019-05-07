import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, retry} from 'rxjs/operators';
import { ApiService } from '../apiService/api.service';

@Injectable({
  providedIn: 'root'
})
export class QuestionTypeService {

  constructor(private httpClient: HttpClient, private apiService: ApiService ) {}
  get() {
    return this.httpClient.get(this.apiService.baseUrl + 'questionType').pipe(
      retry(0), // retry a failed request up to 3 times
      catchError(this.apiService.handleError)
    );
  }
}

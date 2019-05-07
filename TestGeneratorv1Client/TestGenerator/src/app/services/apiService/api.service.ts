import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { AuthService } from '../authService/auth.service';
import { throwError} from 'rxjs/internal/observable/throwError';



@Injectable()
export class ApiService {

  private API_ROOT = 'http://localhost:60007/api/';
  private token: string | null;
  private header: HttpHeaders;

  constructor(private authService: AuthService, private http: HttpClient) {

    this.setToken();

    if (!this.token) {
      return;
    }
  }

  get baseUrl(): string {
    return this.API_ROOT;
  }

  /**
   * Set Auth token on header request
   */
  setToken(): void {

    this.token = this.authService.getToken();
    this.header = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', 'Bearer ' + this.token as any);
  }

  /**
   * Send get request on API
   *
   * @param url
   * @param params
   * @param fullResponse
   */
  get(url: string, params?: any, fullResponse = false): Observable<{}> {

    if (!this.header.get('Auth')) {
      this.setToken();
    }

    const httpParams: HttpParams = this.generateHttpParams(params);

    let options: {} = Object.assign({}, { headers: this.header, params: httpParams });
    if (fullResponse) {
      options = Object.assign({}, options, {
        observe: 'response'
      });
    }

    return this.http
      .get(this.API_ROOT + url, options);
  }

  /**
   * Send post request on API
   *
   * @param url
   * @param body
   * @param additionalOptions
   * @param queryParams
   */
  post(url: string, body: string, additionalOptions: {} = {}, queryParams?: {}): Observable<{}> {

    let options: {} = Object.assign({}, { headers: this.header });
    if (additionalOptions) {
      options = Object.assign({}, options, additionalOptions);
    }

    const httpParams: HttpParams = this.generateHttpParams(queryParams);

    Object.assign(options, {
      params: httpParams
    });

    return this.http.post(this.API_ROOT + url, body, options);
  }

  /**
   * Send put request on API
   * @param url
   * @param body
   * @returns {Observable<Response>}
   */
  put(url: string, body: string): Observable<{}> {
    return this.http.put(this.API_ROOT + url, body, { headers: this.header });
  }

  /**
   * Send delete request on API
   * @param url
   * @returns {Observable<Response>}
   */
  httpDelete(url: string): Observable<{}> {
    return this.http.delete(this.API_ROOT + url, { headers: this.header });
  }

  private generateHttpParams(params: any): HttpParams {
    let httpParams: HttpParams = new HttpParams();
    if (params) {
      Object.keys(params)
        .forEach((paramName: string) => {
          if (params[paramName]) {
            httpParams = httpParams.append(paramName, params[paramName]);
          }
        });
    }
    return httpParams;
  }
  public handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }

    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }
}

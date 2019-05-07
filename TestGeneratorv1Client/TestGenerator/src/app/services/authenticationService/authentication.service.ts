import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../apiService/api.service';
import { AuthService } from '../authService/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient,
              private apiService: ApiService,
              private authService: AuthService) { }

  login(request) {
    return this.http.post<any>( this.apiService.baseUrl + 'auth/Login', {
      email: request.email,
      password: request.password
    });
  }
  register(request) {
    return this.http.post<any>( this.apiService.baseUrl + 'auth/register', {
      name: request.name,
      email: request.email,
      password: request.password,
      confirmPassword: request.confirmPassword,
      subjects: request.subjects,
      appAdmin: request.appAdmin
    });
  }
  logout(id): void {

    this.http.delete<any>( this.apiService.baseUrl + 'auth/' + id, id).subscribe();
    console.log('df');
    this.authService.removeLoginData();
    location.reload();
  }
}

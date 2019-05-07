import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';

import { StatusService } from './services/statusService/status.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './/app-routing.module';
import { AddQuestionComponent } from './components/add-question/add-question.component';

import { NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { SubjectComponent } from './components/add-question/add-question-subcomponents/subject/subject.component';
import { VisibilityComponent } from './components/add-question/add-question-subcomponents/visibility/visibility.component';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms'; // <-- NgModel lives here
import { Angular2FontawesomeModule } from 'angular2-fontawesome/angular2-fontawesome';
import { NgxEditorModule } from 'ngx-editor';
import { PreviewQuestionComponent } from './components/preview-question/preview-question.component';
import { GenerateTestComponent } from './components/generate-test/generate-test.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';

import { AppComponent } from './app.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { AuthService } from './services/authService/auth.service';
import { ApiService } from './services/apiService/api.service';
import { PublicGuard } from './guards/public.guard';
import { AuthenticationService } from './services/authenticationService/authentication.service';
import { AuthGuard } from './guards/auth.guard';
import { QuestionService } from './services/questionService/question.service';
import { Subject } from 'rxjs';
import { SubjectService } from './services/subjectService/subject.service';
import { AlertModule } from 'ngx-bootstrap';
import { PreviewTestsComponent } from './components/preview-tests/preview-tests.component';
import {NgxPaginationModule} from 'ngx-pagination';
import { HelloComponent } from './components/hello/hello.component';
import { AdminGuard } from './guards/admin.guard';
import { SystemGuard } from './guards/system.guard';

import { EditQuestionComponent } from './components/edit-question/edit-question.component';
import { TestPrintPreviewComponent } from './components/test-print-preview/test-print-preview.component';
import { SystemAdminPanelComponent } from './components/system-admin-panel/system-admin-panel.component';
import { TokenInterceptor } from './components/interceptors/TokenInterceptor';


@NgModule({
  declarations: [
    AppComponent,
    AddQuestionComponent,
    SubjectComponent,
    VisibilityComponent,
    PreviewQuestionComponent,
    GenerateTestComponent,
    LoginComponent,
    RegisterComponent,
    HeaderComponent,
    FooterComponent,
    AdminPanelComponent,
    PreviewTestsComponent,
    HelloComponent,
    EditQuestionComponent,
    TestPrintPreviewComponent,
    SystemAdminPanelComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    FormsModule,
    Angular2FontawesomeModule,
    NgxEditorModule,
    ReactiveFormsModule,
    NgxPaginationModule


  ],
  providers: [
    AuthService,
    ApiService,
    SubjectService,
    QuestionService,
    StatusService,
    AuthGuard,
    PublicGuard,
    AdminGuard,
    SystemGuard,
    AuthenticationService,
    { provide: LOCALE_ID, useValue: 'sr-Latn' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule {

 }


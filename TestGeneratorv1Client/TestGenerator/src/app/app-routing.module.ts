import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddQuestionComponent } from './components/add-question/add-question.component';
import { SubjectComponent } from './components/add-question/add-question-subcomponents/subject/subject.component';
import { PreviewQuestionComponent } from './components/preview-question/preview-question.component';
import { GenerateTestComponent } from './components/generate-test/generate-test.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { PublicGuard } from './guards/public.guard';
import { AuthGuard } from './guards/auth.guard';
import {PreviewTestsComponent} from './components/preview-tests/preview-tests.component';
import { HelloComponent } from './components/hello/hello.component';
import { AdminGuard } from './guards/admin.guard';
import { SystemGuard } from './guards/system.guard';
import { SystemAdminPanelComponent } from './components/system-admin-panel/system-admin-panel.component';

import { EditQuestionComponent } from './components/edit-question/edit-question.component';
import { TestPrintPreviewComponent } from './components/test-print-preview/test-print-preview.component';

const routes: Routes = [

  { path: '', redirectTo: '/login', pathMatch: 'full', canActivate: [PublicGuard] },
  { path: 'login', component: LoginComponent, canActivate: [PublicGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [PublicGuard] },
  { path: 'GenerateTest', component: GenerateTestComponent, canActivate: [AuthGuard] },
  { path: 'AddQuestion', component: AddQuestionComponent, canActivate: [AuthGuard] },
  { path: 'AdminPanel', component: AdminPanelComponent , canActivate: [AdminGuard] },
  { path: 'PreviewQuestion', component: PreviewQuestionComponent, canActivate: [AuthGuard] },
  { path: 'PreviewTests', component: PreviewTestsComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/StartPage', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'StartPage', component: HelloComponent, canActivate: [AuthGuard] },
  { path: 'EditQuestion', component: EditQuestionComponent, canActivate: [AuthGuard] },
  { path: 'testPreview', component: TestPrintPreviewComponent, canActivate: [AuthGuard] },
  { path: 'systemAdminPanel', component: SystemAdminPanelComponent, canActivate: [SystemGuard] },


];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]

})
export class AppRoutingModule { }


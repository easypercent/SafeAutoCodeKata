import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UploadComponent } from "./upload/upload.component";
import { RulesComponent } from "./rules/rules.component";

const routes: Routes = [
  { path: '', component: UploadComponent },
  { path: 'upload', component: UploadComponent },
  { path: 'rules', component: RulesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

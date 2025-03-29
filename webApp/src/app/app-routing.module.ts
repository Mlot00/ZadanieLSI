import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExportListComponent } from './features/Export/export-list/export-list.component';
import { AddExportComponent } from './features/Export/add-export/add-export.component';

const routes: Routes = [
  {
    path: '',
    component: ExportListComponent
  },
  {
    path: 'export/add',
    component: AddExportComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

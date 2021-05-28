import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RushingTableComponent } from './_components/rushing-table/rushing-table.component';

/**
 * set routes. redirect to table component if path not matched
 */
const routes: Routes = [
  { path: '', component: RushingTableComponent },
  { path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

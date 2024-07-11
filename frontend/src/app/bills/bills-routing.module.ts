import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BillListPage } from './pages/bill-list-page/bill-list.page';

const routes: Routes = [
  {
    path: '',
    component: BillListPage,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BillsRoutingModule { }

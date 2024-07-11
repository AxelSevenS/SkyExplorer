import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BillsRoutingModule } from './bills-routing.module';
import { BillListPage } from './pages/bill-list-page/bill-list.page';
import { BillComponent } from './components/bill/bill.component';
import { MatCardModule } from '@angular/material/card';
import { MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { CourseComponent } from '../courses/components/course/course.component';
import { CoursesModule } from '../courses/courses.module';



@NgModule({
  imports: [
    BillsRoutingModule,
    MatLabel,
    MatCardModule,
    CommonModule,
    MatIcon,
    MatMenuModule,
    MatSelectModule,
  ],
  exports: [
    BillListPage,
    BillComponent,
  ],
  declarations: [
    BillListPage,
    BillComponent,
  ],
})
export class BillsModule { }

import { CoursesRoutingModule } from './courses-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';

import { CreateCoursePage } from './pages/create-course-page/create-course.page';
import { CourseTimelineComponent } from './components/course-timeline/course-timeline.component';
import { MatSelectModule } from '@angular/material/select';
import { CourseComponent } from './components/course/course.component';
import { CourseListPage } from './pages/course-list-page/course-list.page';
import { CoursePage } from './pages/course-page/course.page';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
  imports: [
    CoursesRoutingModule,

    CommonModule,
		FormsModule,
		ReactiveFormsModule,

		MatCardModule,
		MatToolbarModule,
		MatFormFieldModule,
		MatOptionModule,
		MatSelectModule,
		MatMenuModule,
		MatDatepickerModule,
		MatCheckboxModule,
		MatNativeDateModule,
		MatInputModule,
		MatButtonModule,
		MatGridListModule,
		MatLabel,
		MatIcon,
  ],
  declarations: [
		CoursePage,
		CourseListPage,
		CreateCoursePage,
		CourseComponent,
		CourseTimelineComponent,
	],
  exports: [
		CoursePage,
		CourseListPage,
		CreateCoursePage,
		CourseComponent,
		CourseTimelineComponent,
	],
	providers: [
		MatDatepickerModule,
	]
})
export class CoursesModule { }

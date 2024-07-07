import { CoursesRoutingModule } from './courses-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';

import { CoursesPage } from './pages/courses-page/courses.page';
import { CreateCoursePage } from './pages/create-course-page/create-course.page';
import { CourseTimelineComponent } from './components/course-timeline/course-timeline.component';

@NgModule({
  imports: [
    CoursesRoutingModule,

    CommonModule,
		FormsModule,
		ReactiveFormsModule,

		MatCardModule,
		MatToolbarModule,
		MatInputModule,
		MatButtonModule,
		MatGridListModule,
		MatFormFieldModule,
		MatLabel,
		MatIcon,
  ],
  declarations: [
		CoursesPage,
		CreateCoursePage,
		CourseTimelineComponent,
	],
  exports: [
		CoursesPage,
		CreateCoursePage,
		CourseTimelineComponent,
	],
})
export class CoursesModule { }

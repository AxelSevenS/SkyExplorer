import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoursesPage } from './pages/courses-page/courses.page';
import { CreateCoursePage } from './pages/create-course-page/create-course.page';

const routes: Routes = [
	{
		path: '',
		component: CoursesPage,
	},
	{
		path: 'create',
		component: CreateCoursePage
	}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoursesRoutingModule { }

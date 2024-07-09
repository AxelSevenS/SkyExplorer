import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CourseListPage } from './pages/course-list-page/course-list.page';
import { CreateCoursePage } from './pages/create-course-page/create-course.page';
import { CoursePage } from './pages/course-page/course.page';

const routes: Routes = [
	{
		path: 'list',
		component: CourseListPage,
	},
	{
		path: 'create',
		component: CreateCoursePage
	},
	{
		path: ':id',
		component: CoursePage,
	},

	{ path: '', redirectTo: 'list', pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoursesRoutingModule { }

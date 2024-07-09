import { Component, Input, OnInit, numberAttribute } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { Course, CourseCreateDto, CourseUpdateDto } from '../../models/course.model';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';

@Component({
	selector: 'se-course',
	templateUrl: './course.component.html',
	styleUrls: ['./course.component.scss'],
})
export class CourseComponent extends EntityViewComponent<Course, CourseCreateDto, CourseUpdateDto> {
	constructor(
		public override authentication: AuthenticationService,
		protected override entityService: CourseService,
	) {
		super(authentication, entityService)
	}
}
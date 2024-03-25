import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-category-badge',
  standalone: true,
  imports: [],
  templateUrl: './category-badge.component.html'
})
export class CategoryBadgeComponent {
  @Input() category?: string;
}

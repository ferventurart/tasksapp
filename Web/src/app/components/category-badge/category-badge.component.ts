import { Component, Input, input } from '@angular/core';

@Component({
  selector: 'app-category-badge',
  standalone: true,
  imports: [],
  templateUrl: './category-badge.component.html',
  styleUrl: './category-badge.component.css'
})
export class CategoryBadgeComponent {
  @Input() category?: string;
}

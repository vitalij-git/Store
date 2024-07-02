import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: "./paging-header.component.html",
  styles: [
  ]
})
export class PagingHeaderComponent {
  @Input() pageNumber?: number;
  @Input() totalCount?: number;
  @Input() pageSize?: number;
}

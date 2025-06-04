import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-generic-delete',
  templateUrl: './generic-delete.component.html',
  styleUrls: ['./generic-delete.component.css']
})
export class GenericDeleteComponent<T> implements OnInit {
  @Input() title!: string;
  @Input() getItems!: () => Observable<T[]>;
  @Input() deleteItem!: (id: string) => Observable<any>;

  items: T[] = [];
  selectedId: string = '';
  message = '';
  error = '';

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    this.getItems().subscribe({
      next: data => this.items = data,
      error: err => this.error = 'Error loading items.'
    });
  }

  onDelete(): void {
    this.message = '';
    this.error = '';
    if (!this.selectedId) return;

    this.deleteItem(this.selectedId).subscribe({
      next: res => {
        this.message = res?.message ?? 'Deleted successfully.';
        this.loadItems(); // Refresh list
      },
      error: err => {
        this.error = err?.error?.message ?? 'Delete failed.';
      }
    });
  }
}

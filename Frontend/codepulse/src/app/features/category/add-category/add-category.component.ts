import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';
import { Router} from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-add-category',
  imports: [FormsModule,CommonModule],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent implements OnDestroy {
  private categoryService = inject(CategoryService);
  private router=inject(Router)
  private addCategorySubscription?: Subscription;

  model:AddCategoryRequest;
  constructor() {
    this.model = {
      Name: '',
      UrlHandle: ''
    };
  }
 
  onFormSubmit() {
    this.addCategorySubscription=this.categoryService.addCategory(this.model).subscribe({
      next: () => {
        this.router.navigate(['/admin/categories']);
        this.model = { Name: '', UrlHandle: '' }; // Reset the form
      },
      error: (err) => {
        console.error('Error adding category:', err);
        alert('Failed to add category');
      }
    });
  }

  ngOnDestroy() {
    this.addCategorySubscription?.unsubscribe();
  }
}

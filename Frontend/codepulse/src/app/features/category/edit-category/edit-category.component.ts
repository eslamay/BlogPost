import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Component({
  standalone: true,
  selector: 'app-edit-category',
  imports: [FormsModule,CommonModule],
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css'
})
export class EditCategoryComponent implements OnInit {
  private routerActivated= inject(ActivatedRoute);
  private categoryService = inject(CategoryService);
  private router = inject(Router);
  categoryId: string | null = null;
  category?: Category;
  ngOnInit(): void {
    this.categoryId = this.routerActivated.snapshot.paramMap.get('id');
    if (this.categoryId) {
      this.categoryService.getCategoryById(this.categoryId).subscribe({
        next: (category) => {
          this.category =category;
        },
        error: (error) => {
          console.error('Error fetching category:', error);
        }
      });
    }
  }

  onFormSubmit(): void {
   const updatedCategory: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? '',
    };

    if (this.categoryId) {
      this.categoryService.updateCategory(this.categoryId, updatedCategory).subscribe({
        next: () => {
          this.router.navigate(['/admin/categories']);
        },
        error: (error) => {
          console.error('Error updating category:', error);
        }
      });
    }
  }

  onDelete(): void {
    if (this.categoryId) {
      this.categoryService.deleteCategory(this.categoryId).subscribe({
        next: () => {
          this.router.navigate(['/admin/categories']);
        },
        error: (error) => {
          console.error('Error deleting category:', error);
        }
      });
    }
  }

}

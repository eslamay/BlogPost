import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ImageService } from './image.service';
import { BlogImage } from './models/blog-image.model';

@Component({
  selector: 'app-image-selector',
  imports: [FormsModule],
  templateUrl: './image-selector.component.html',
  styleUrl: './image-selector.component.css'
})
export class ImageSelectorComponent implements OnInit {
  private file?: File;
  fileName: string = '';
  title: string = '';
  private imageService = inject(ImageService);
  allImages: BlogImage[] = [];

  @ViewChild('form', { static: false }) imageUploadForm?: NgForm;

  ngOnInit(): void {
    this.imageService.getAllImages().subscribe({
      next: (images) => {
        this.allImages = images;
      },
      });
  }

  onImageSelected(image: BlogImage): void {
    this.imageService.setSelectedImage(image);
  }

  OnFileUploadChange(event: Event): void {
    const element = event.target as HTMLInputElement;
      this.file = element.files?.[0];
    }

    onFormSubmit()
    {
       if(this.file&&this.fileName!==''&&this.title!=='')
       {
         this.imageService.UploadImage(this.file,this.fileName,this.title).subscribe({
           next: (response) => {
              this.imageUploadForm?.resetForm();
              this.loadImages();
           },
           error: (error) => {
             console.error('Error uploading image:', error);
           }
         });
       }
    }

    loadImages(){
      this.imageService.getAllImages().subscribe({
        next: (images) => {
          this.allImages = images;
        },
        error: (error) => {
          console.error('Error loading images:', error);
        }
      });
    }
  }


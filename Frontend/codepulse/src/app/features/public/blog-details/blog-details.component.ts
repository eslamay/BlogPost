import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { AsyncPipe, CommonModule } from '@angular/common';
import { MarkdownComponent } from "ngx-markdown";

@Component({
  selector: 'app-blog-details',
  imports: [CommonModule, MarkdownComponent],
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css',
  providers:[AsyncPipe]
})
export class BlogDetailsComponent implements OnInit {
  private activatedRoute= inject(ActivatedRoute);
  private blogService = inject(BlogPostService);
  urlHandle: string | null = null;
  blogPost$?: Observable<BlogPost>;
  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      this.urlHandle = params.get('url');
    });

    if (this.urlHandle) {
      this.blogPost$ = this.blogService.getBlogPostByUrlHandle(this.urlHandle);
    } 
  }

}

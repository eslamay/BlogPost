import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogListComponent } from './features/blog-post/blog-list/blog-list.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';
import { EditBlogpostComponent } from './features/blog-post/edit-blogpost/edit-blogpost.component';
import { HomeComponent } from './features/public/home/home.component';
import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/auth.guard';
import { RegisterComponent } from './features/auth/register/register.component';

export const routes: Routes = [
    {
        path:'',
        component:HomeComponent
    },
    {
        path:'blog/:url',
        component: BlogDetailsComponent
    },
    {
        path:'admin/categories',
        component: CategoryListComponent,
        canActivate:[authGuard]
    },
    {
        path:'admin/categories/add',
        component:AddCategoryComponent,
        canActivate:[authGuard]
    },
    {
       path:'admin/categories/:id',
       component:EditCategoryComponent,
        canActivate:[authGuard]
    },
    {
        path:'admin/BlogPosts',
        component: BlogListComponent,
        canActivate:[authGuard]
    },
    {
        path:'admin/BlogPosts/add',
        component: AddBlogpostComponent,
        canActivate:[authGuard]
    },
    {
        path:'admin/BlogPosts/:id',
        component: EditBlogpostComponent,
        canActivate:[authGuard]
    },
    {
        path:'login',
        component: LoginComponent
    },
    {
        path:'register',
        component: RegisterComponent
    }
];

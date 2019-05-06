import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component'; 

//This is my case 
const routes: Routes = [
    {
        path: '',
        component: AppComponent
    },  
    {
        path: 'about',
        component: AppComponent
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
import { Component , ViewChild,ViewChildren, QueryList, ElementRef} from '@angular/core';
import { Store } from '@ngrx/store';
import { ActionsUnion, Login } from './actions/Login';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})

export class AppComponent {

  title = 'AzInsurance';
 
  @ViewChildren("div") divs: QueryList<any>;

  constructor(private store : Store<ActionsUnion>) { 
     
  }

  ngAfterViewInit() {
    console.log(this.divs);
  }

  run() {     
    this.store.dispatch(new Login());
  }
}


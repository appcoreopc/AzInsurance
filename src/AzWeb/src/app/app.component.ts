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
  image = "https://s3.amazonaws.com/site-files-prod/FiftyFlowers/Image/Product/Mini-Black-Eye-bloom-350_c7d02e72.jpg";
  showIt = true; 
  imageWidth = 1000;
  badCurly = "really curly"
  
  @ViewChildren("div") divs: QueryList<any>;

  constructor(private store : Store<ActionsUnion>) { 
     
  }

  ngAfterViewInit() {
    console.log(this.divs);
  }

  public execute(evt) {
    console.log('button hit!', evt);
  }

  run() { 
    console.log("tet")
    this.store.dispatch(new Login());
  }
}


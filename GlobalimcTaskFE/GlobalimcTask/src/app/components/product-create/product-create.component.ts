import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DietaryFlags } from 'src/app/enums/dietary-flags.enum';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss']
})
export class ProductCreateComponent implements OnInit {
  create_form: FormGroup;
  
  submitted = false;
  response= {dbPath: ''};

  constructor(private productService: ProductService,fb: FormBuilder) {
    this.create_form = fb.group({
      'VendorId': [null, Validators.required],
      'Title': [null, Validators.required],
      'Description': [null, Validators.required],
      'Price': [null, Validators.required],
      'DietaryFlag': [null, Validators.required],
      'ViewsCount': [null, Validators.required],
     
    });
  }
  ngOnInit(): void {
  }
  public uploadFinished = (event) => {
    this.response = event;
  }
  markFormTouched(group: FormGroup | FormArray) {
    Object.keys(group.controls).forEach((key: string) => {
      const control = group.controls[key];
      if (control instanceof FormGroup || control instanceof FormArray) { control.markAsTouched(); this.markFormTouched(control); }
      else { control.markAsTouched(); };
    });
  };
  createProduct(): void {
    debugger
    this.markFormTouched(this.create_form);
    
    if (this.create_form.valid) {
     
      var data = this.create_form.getRawValue();
      data["Image"]=this.response.dbPath;
      data["DietaryFlag"]= +data["DietaryFlag"];
      this.productService.create(data)
        .subscribe(
          response => {
            console.log(response);
            this.submitted = true;
          },
          error => {
            console.log(error);
          });

    } 
    
  }

  newProduct(): void {
    this.submitted = false;
    this.create_form.reset();
  }

}
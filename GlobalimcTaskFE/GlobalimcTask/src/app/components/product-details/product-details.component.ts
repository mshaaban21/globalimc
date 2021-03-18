import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  currentProduct = null;
  message = '';
  img='';
  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }
  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.message = '';
    this.getProduct(this.route.snapshot.paramMap.get('id'));
  }

  getProduct(id): void {
    this.productService.read(id)
      .subscribe(
        product => {
          this.currentProduct = product;
          if(product.Image != undefined  && product.Image.length>0){
            this.img="https://globalimctaskbe20210318215446.azurewebsites.net/"+product.Image;
          }
          console.log(product);
        },
        error => {
          console.log(error);
        });
  }

  
  updateProduct(): void {
    this.productService.update(this.currentProduct.Id, this.currentProduct)
      .subscribe(
        response => {
          console.log(response);
          if(response){
            this.message = 'The product was updated!';
          }
         
        },
        error => {
          console.log(error);
        });
  }

  deleteProduct(): void {
    this.productService.delete(this.currentProduct.Id)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/products']);
        },
        error => {
          console.log(error);
        });
  }
}
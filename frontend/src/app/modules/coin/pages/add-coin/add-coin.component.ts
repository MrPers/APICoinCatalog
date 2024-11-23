import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-coin',
  templateUrl: './add-coin.component.html',
  styleUrls: ['./add-coin.component.css']
})

export class AddCoinComponent implements OnInit {  
  @Output() close = new EventEmitter<void>();
  @Output() dateTime = "2020-01-01";
  @Output() coinName = "";

  myForm : FormGroup;
  private maxData = new Date().toLocaleString().split(',')[0].split('/');
  public maxDataString = this.maxData[2] + "-" + this.maxData[1] + "-" + this.maxData[0];

  constructor() {}

  ngOnInit(): void {
    this.myForm = new FormGroup({              
      "coinName": new FormControl("", [
        Validators.required,
        Validators.maxLength(10)])
    });
  }

  submit(){
    this.coinName = this.myForm.controls['coinName'].value;
    this.close.emit();
  }
}

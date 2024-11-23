// import { outputAst } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-refresh-coin',
  templateUrl: './refresh-coin.component.html',
  styleUrls: ['./refresh-coin.component.css']
})

export class RefreshCoinComponent {

  @Input() name: string;
  @Output() close = new EventEmitter<void>();
  @Output() send = false;

  submit(){
    this.send = true;
    this.close.emit();
  }

}

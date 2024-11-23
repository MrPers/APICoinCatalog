import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-delete-coin',
  templateUrl: './delete-coin.component.html',
  styleUrls: ['./delete-coin.component.css']
})

export class DeleteCoinComponent {

  @Input() name: string;
  @Output() close = new EventEmitter<void>();
  @Output() send = false;

  submit(){
    this.send = true;
    this.close.emit();
  }

}
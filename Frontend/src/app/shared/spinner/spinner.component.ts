import { Component } from '@angular/core';
import { LoadingService } from '../../services/components/loading.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-spinner',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent {
  constructor(public loadingService: LoadingService) {}
}
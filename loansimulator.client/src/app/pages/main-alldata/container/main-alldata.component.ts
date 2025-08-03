import { Component, OnInit } from '@angular/core';
import { LoanSimResponsedata } from '../../../shared/interface/LoanSimResponsedata';
import { ApiServiceService } from '../../../shared/services/api-service.service';
import { PagedResponse } from '../../../shared/interface/PagedResponse';

@Component({
  selector: 'app-main-alldata',
  standalone: false,
  templateUrl: './main-alldata.component.html',
  styleUrl: './main-alldata.component.css'
})
export class MainAlldataComponent implements OnInit {
  loans: LoanSimResponsedata[] = [];
  pageNumber = 1;
  pageSize = 10;
  totalPages = 1;
  errorMessage: string = '';

  constructor(private apiService: ApiServiceService) { }

  ngOnInit() {
    this.loadLoans();
  }

  loadLoans() {
    this.apiService.getAllLoans(this.pageNumber, this.pageSize).subscribe({
      next: (data: PagedResponse<LoanSimResponsedata>) => {
        this.loans = data.items;
        this.totalPages = data.totalPages;
        this.errorMessage = '';
      },
      error: (error) => {
        this.errorMessage = error.message || 'Error loading loans';
      }
    });
  }

  onPageChange(newPage: number) {
    if (newPage < 1 || newPage > this.totalPages) return;
    this.pageNumber = newPage;
    this.loadLoans();
  }

  onPageSizeChange(newSize: number) {
    this.pageSize = +newSize;
    this.pageNumber = 1;
    this.loadLoans();
  }
}

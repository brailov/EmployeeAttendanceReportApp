import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PersonService } from '../../services/person.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Report } from '../../model/report.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-report-popup',
  standalone: false,  
  templateUrl: './report-popup.component.html',
  styleUrl: './report-popup.component.css'
})
export class ReportPopupComponent {

  response$: Observable<any> | undefined; 
  
  _reportForm: FormGroup = new FormGroup({
    date: new FormControl(<Date | null>(null), Validators.required),
    startTime: new FormControl(<Date | null>(null), Validators.required),
    endTime: new FormControl(<Date | null>(null), Validators.required)
  })

  constructor(public dialogRef: MatDialogRef<ReportPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: number },
    private personService: PersonService) { }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {   
    if (this._reportForm.valid) {
      if (this.compareTimes(this.startTime, this.endTime)) { alert('Start Time must be before End Time.'); return; }

      let _data: Report = {
        date: this._reportForm.value.date.toLocaleDateString('en-GB'),
        startTime: this._reportForm.value.startTime as string,
        endTime: this._reportForm.value.endTime as string
      }

      this.personService.createPersonReport(this.data.id ,_data).subscribe({
        next: (response) => {       
          if (response != null)
            this.response$ = response;         
        },
        error: (error) => {                 
        }
      });
    }
    this.dialogRef.close();
  }

  compareTimes(startTime: string, endTime: string): boolean {
    const start = new Date(`1970-01-01 ${startTime}`);
    const end = new Date(`1970-01-01 ${endTime}`);
    return start >= end;
  }

  get startTime() {
    return this._reportForm.value.startTime as string;
  }

  get endTime() {
    return this._reportForm.value.endTime as string;
  }
}

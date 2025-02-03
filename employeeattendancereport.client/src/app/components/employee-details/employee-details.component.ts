import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, of, switchMap, catchError } from 'rxjs';
import { ReportStatus, Role } from '../../model/enums.model';
import { Person } from '../../model/person.model';
import { PersonReport } from '../../model/personReport.model';
import { PersonService } from '../../services/person.service';
import { ReportService } from '../../services/report.service';
import { ReportPopupComponent } from '../report-popup/report-popup.component';

@Component({
  selector: 'app-employee-details',
  standalone: false,
  
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css'
})
export class EmployeeDetailsComponent implements OnChanges {
  @Input() selectedItem: Person | null = null; 

  _selectedPerson: Person = { id: 0, firstName: '', lastName: '', manager: '', role: '' };
  _isSelectedPersonManager!: boolean;
  _isRowSeleced: boolean = false;
  _personReports$: Observable<PersonReport[]> = of([]);
  reportStatus = ReportStatus; // Reference the enum
  role = Role;

  constructor(private personService: PersonService, private reportService: ReportService, public dialog: MatDialog) {
    this._isSelectedPersonManager = true;
  }

  ngOnChanges(changes: SimpleChanges): void { // it is called whenever @Input() property change.
    
    if (changes['selectedItem']) {
      if (this.selectedItem) {    
        this._selectedPerson = this.selectedItem;
        this._isRowSeleced = true;
        this.loadManagerAndReports(this.selectedItem.id);        
      }
    }
  }

  loadManagerAndReports(personId: number) {
    this.personService.getManager(personId)
      .pipe(
        switchMap((manager) => {
          if (manager) {
            this._isSelectedPersonManager = true;
            return this.personService.getPersonReports(manager.id);
          } else {
            this._isSelectedPersonManager = false;
            return of([]);
          }
        }),
        catchError((error) => {                   
          return of([]); // Return an empty array on error
        })
      )
      .subscribe({
        next: (reports) => {
          this._personReports$ = of(reports);
        },
        error: (err) => {                  
        }
      });
  }

  onClockInOut(): void {
    this.dialog.open(ReportPopupComponent, {
      width: '500px', height: '450px',
      data: { id: this._selectedPerson.id }
    });
  
  }

  updateReportStatus(id: string, newStatus: ReportStatus, personId: number) {

    this.reportService.updateReports(id, newStatus).subscribe({
      next: (respose) => {
        if (respose) {
          this.loadManagerAndReports(personId);
        };
      },
      error: (err) => {              
      }
    });
  }
}

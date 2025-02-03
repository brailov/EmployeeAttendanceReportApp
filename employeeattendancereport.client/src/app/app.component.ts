import { Component, OnInit, ViewChild } from '@angular/core';
import { Person } from './model/person.model';
import { PersonService } from './services/person.service';
import { Observable, of, take } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  _persons$: Observable<Person[]> = of([]);
  _selectedPerson: Person | null = null; 

  constructor(private personService: PersonService) { }

  ngOnInit() {
    this.getAllPersons();
  }

  getAllPersons() {
    this.personService.getAllPersons().subscribe({
      next: (items) => {
        this._persons$ = of(items);
     
      },
      error: (err) => {       
        console.error('Error loading persons table:', err);    
      }
    }); 
  }

  showEmployeeDetails(id: number)
  {
    this._persons$.pipe(take(1)).subscribe(p =>{      
      const person = p.find(p => p.id === id);
      if (person == undefined) return;  
      this._selectedPerson = person;    
    });     
  } 
}

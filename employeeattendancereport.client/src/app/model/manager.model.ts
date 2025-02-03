import { Person } from "./person.model";

export interface Manager extends Person
{
  employees: Person[];
  subManagers: Person[];
}

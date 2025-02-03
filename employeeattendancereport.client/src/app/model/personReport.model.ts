import { Report } from "./report.model";

export interface PersonReport extends Report {
  id: string;
  personId: number;
  name: string;
}

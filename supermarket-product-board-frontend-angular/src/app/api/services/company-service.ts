import { inject, Injectable } from '@angular/core';
import { Client, Company } from '../generated/stp-client';
import { lastValueFrom } from 'rxjs';

@Injectable()
export class CompanyService {
  private client = inject(Client);

  getCompanies(): Promise<Company[]> {
    return lastValueFrom(this.client.getCompanies());
  }
}

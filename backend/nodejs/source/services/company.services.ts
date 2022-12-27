import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, company } from "../entities";
import { CompanyType } from "../enums";
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";

interface localCompany {
  company_name: string;
  site: string;
  email: string;
  company_type: CompanyType;
}

interface ICompanyService {
  getCompanies(): Promise<company[]>;
}

export class CompanyService implements ICompanyService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public getCompanies(): Promise<company[]> {
    return new Promise<company[]>((resolve, reject) => {
      const result: company[] = [];

      SqlHelper.executeQueryArrayResult<localCompany>(
        this._errorService,
        Queries.Companies
      )
        .then((queryResult: localCompany[]) => {
          queryResult.forEach((company: localCompany) => {
            result.push(this.parseLocalCompany(company));
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  public addCompany(company: company): Promise<company> {
    return new Promise<company>((resolve, reject) => {
      SqlHelper.createNew(
        this._errorService,
        Queries.AddCompany,
        company,
        company.company_name,
        company.site,
        company.email,
        company.company_type,
      )
        .then((result: company) => {
          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  private parseLocalCompany(local: localCompany): company {
    return {
      company_name: local.company_name,
      site: local.site,
      email: local.email,
      company_type: local.company_type,
    };
  }
}

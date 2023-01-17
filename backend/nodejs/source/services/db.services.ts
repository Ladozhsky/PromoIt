import * as _ from "underscore";
import { systemError} from '../entities';
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";

interface IDbService {
  getAllCollumnData<T>(query : string) : Promise<T[]>;
}
export class DbGetService implements IDbService {

  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public getAllCollumnData<T>(query : string) : Promise<T[]> {
    return new Promise<T[]>((resolve, reject) => {
      const result: T[] = [];

      SqlHelper.executeQueryArrayResult<T>(
        this._errorService,
        query
      )
        .then((queryResult: T[]) => {
          queryResult.forEach((aray: T) => {
            result.push(aray);
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }
}

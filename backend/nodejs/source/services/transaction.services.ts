import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, transaction } from "../entities";
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";

interface ITransactionService {
  addTransaction(transaction: transaction): Promise<transaction>
}

export class TransactionService implements ITransactionService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public addTransaction(transaction: transaction): Promise<transaction> {
    return new Promise<transaction>((resolve, reject) => {
      SqlHelper.createNew(
        this._errorService,
        Queries.AddTransaction,
        transaction,
        transaction.user_id,
        transaction.campaign_id,
        transaction.amount,
        transaction.reason,
        transaction.retweet_id,
        transaction.create_date,
        transaction.update_date,
        transaction.create_by_user,
        transaction.update_by_user,
      )
      .then((result: transaction) => {
        resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }
}

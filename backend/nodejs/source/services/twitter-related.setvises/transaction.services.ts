import * as _ from "underscore";
import { Queries } from "../../constants";
import { systemError, transaction } from "../../entities";
import { SqlHelper } from "../../helpers/sql.helper";
import { ErrorService } from "../error.service";

interface localTransaction {
    user_id: string ;
    campaign: string;    
    amount: number;
    reason: string;
    retweet_id: number;
    creation_date: Date;
    update_date: Date;
    create_by_user: string;   
    update_by_user: string;     
}

interface ITransactionService {
  getTransactions(): Promise<transaction[]>;
  getTransactionsByUserId(userId: number): Promise<transaction[]>;
  addTransaction(transaction: transaction): Promise<transaction>
}

export class TransactionService implements ITransactionService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public getTransactions(): Promise<transaction[]> {
    return new Promise<transaction[]>((resolve, reject) => {
      const result: transaction[] = [];

      SqlHelper.executeQueryArrayResult<localTransaction>(
        this._errorService,
        Queries.Transactions
      )
        .then((queryResult: localTransaction[]) => {
          queryResult.forEach((transaction: localTransaction) => {
            result.push(this.parselocalTransaction(transaction));
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  public getTransactionsByUserId(userId: number): Promise<transaction[]> {
    return new Promise<transaction[]>((resolve, reject) => {
      const result: transaction[] = [];

      SqlHelper.executeQueryArrayResult<localTransaction>(
        this._errorService,
        Queries.TransactionsByUserId,
        userId
      )
        .then((queryResult: localTransaction[]) => {
          queryResult.forEach((transaction: localTransaction) => {
            result.push(this.parselocalTransaction(transaction));
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  public addTransaction(transaction: transaction): Promise<transaction> {
    return new Promise<transaction>((resolve, reject) => {
      SqlHelper.createNew(
        this._errorService,
        Queries.AddTransaction,
        transaction,
        transaction.user_id,
        transaction.campaign,
        transaction.amount,
        transaction.reason,
        transaction.retweet_id,
        transaction.creation_date,
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

  private parselocalTransaction(local: localTransaction): transaction {
    return {
        user_id: local.user_id,
        campaign: local.campaign,
        amount: local.amount,
        reason: local.reason,
        retweet_id: local.retweet_id,
        creation_date: local.creation_date,
        update_date: local.update_date,
        create_by_user: local.create_by_user,
        update_by_user: local.update_by_user,
    };
  }
}

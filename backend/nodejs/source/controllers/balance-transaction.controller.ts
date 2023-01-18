 import { systemError, transaction } from '../entities';
import { ErrorService } from '../services/error.service';
import { TransactionService } from '../services/transaction.services';
import { RetweetService } from '../services/retweet.services';

const errorService: ErrorService = new ErrorService();
const transactionService: TransactionService = new TransactionService(errorService);
const retweetService: RetweetService = new RetweetService(errorService);

const addTransactions = async (transactionListInput: Promise<transaction[]>) => {
  const transactionList : transaction[] = await transactionListInput
  for (let i = 0; i < transactionList.length; i++) {
      transactionService.addTransaction({
        user_id: transactionList[i].user_id,
        campaign_id: transactionList[i].campaign_id,
        amount: transactionList[i].amount,
        reason: transactionList[i].reason,
        retweet_id: transactionList[i].retweet_id,
        create_date: new Date(),
        update_date: new Date(),
        create_by_user: transactionList[i].create_by_user,
        update_by_user: transactionList[i].update_by_user,
      })
      retweetService.updateRetweetByScript(transactionList[i].retweet_id)
        .then(() => {
          return console.log(transactionList[i]);
      })
        .catch((error: systemError) => {
          return console.log(error);
      });
    }
  }

  export default (addTransactions)

  
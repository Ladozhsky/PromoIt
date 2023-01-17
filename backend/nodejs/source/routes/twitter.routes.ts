import express from 'express';
import controller from '../controllers/twitter.controller';

const router = express.Router();

router.post('/', controller.postTweet);

export default { router };
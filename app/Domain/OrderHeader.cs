using System;

namespace Domain
{
    public class OrderHeader : OrderState
    {

        /// <summary>
        /// The Date and Time when the OrderHeader was created
        /// </summary>
        public DateTime DateTime
        {
            get; set;
        }

        /// <summary>
        /// The Id of the OrderHeader
        /// In other words, the unique identifier of the order header
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The state of the OrderHeader. Can either be 1,2,3,4
        /// 1 = New
        /// 2 = Pending
        /// 3 = Rejected
        /// 4 = Complete
        /// </summary>
        public int State
        {
            get; set;
        }


        /// <summary>
        /// Constructor for the OrderHeader class
        /// </summary>
        /// <param name="DateTime">The Date and Time when the OrderHeader was created</param>
        /// <param name="Id"></param>
        /// <param name="State">The state of the OrderHeader. Can either be 1,2,3,4</param>
        public OrderHeader(DateTime DateTime, int Id, int State)
        {
            this.DateTime = DateTime;
            this.Id = Id;
            this.State = State;
        }


        //I did not end up needing to use functions/properties below
        //These functions/properties were implemented in either AddOrder_Window or AddOrderItem_Window


        //private readonly string _orderItems;


        //public double Total
        //{
        //    get; set;
        //}


        //public void AddOrderItem()
        //{


        //}
        //public void Complete()
        //{


        //}

        //public void OrderHeader_()
        //{


        //}


        //public void Reject()
        //{


        //}

        //public OrderState StateId
        //{
        //    get;
        //    set;
        //}


        //private void setState(int id)
        //{
        //    //selecting the correct enumerator with the id
        //    OrderStates enum_selected = (OrderStates)id;


        //    switch (enum_selected)
        //    {
        //        case OrderStates.New:
        //            StateId = new OrderNew(this);
        //            break;
        //        case OrderStates.Pending:
        //            StateId = new OrderPending(this);
        //            break;
        //        case OrderStates.Rejected:
        //            StateId = new OrderRejected(this);
        //            break;
        //        case OrderStates.Complete:
        //            StateId = new OrderComplete(this);
        //            break;
        //    }
        //}

        //public void Submit()
        //{

        //}

    }
}

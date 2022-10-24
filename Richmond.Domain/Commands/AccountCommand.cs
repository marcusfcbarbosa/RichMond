using FluentValidator;
using FluentValidator.Validation;
using MediatR;
using Richmond.Domain.Entities;
using Richmond.Domain.Events;
using Richmond.Domain.Interfaces;
using Richmond.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Richmond.Domain.Commands
{
    public class AccountCommand : Notifiable, ICommand, IRequest<ICommandResult>
    {
        public string addressBody { get; set; }
        public string aref { get; set; }
        public string authorisedBank { get; set; }
        public string authorisedCard { get; set; }
        public Guid campaignId { get; set; }
        public Guid governmentID { get; set; }
        public Guid lenderId { get; set; }
        public Guid originatorId { get; set; }
        public Decimal productSetupFee { get; set; }
        public DateTime dateOfBirth { get; set; }
        public DateTime? regularPaymentDay { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public decimal monthlyFee { get; set; }
        public string postcode { get; set; }
        public string state { get; set; }
        public string surname { get; set; }
        public string tel { get; set; }

        public decimal expectedPaymentAmount { get; set; }
        public string contract { get; set; }
        public void Validate()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                    .IsNotNullOrEmpty(addressBody, "addressBody", "addressBody is mandatory")
                    .IsNotNullOrEmpty(addressBody, "aref", "aref is mandatory")
                    .IsTrue(!CheckIfIsEmpty(campaignId.ToString()), campaignId.ToString(), "campaignId is mandatory")
                    .IsTrue(!CheckIfIsEmpty(governmentID.ToString()), governmentID.ToString(), "governmentID is mandatory")
                    .IsTrue(!CheckIfIsEmpty(lenderId.ToString()), lenderId.ToString(), "lenderId is mandatory")
                    .IsTrue(!CheckIfIsEmpty(originatorId.ToString()), originatorId.ToString(), "originatorId is mandatory")
                    .IsEmail(email, "Email", "invalid e-mail")
                    .IsNotNullOrEmpty(firstName, "firstName", "firstName is mandatory")
                    .IsNotNullOrEmpty(middleName, "middleName", "middleName is mandatory")
                    .IsLowerOrEqualsThan(monthlyFee, 0, "monthlyFee", "monthlyFee is mandatory")
                    .IsNotNullOrEmpty(postcode, "postcode", "postcode is mandatory")
                    .IsNotNullOrEmpty(state, "state", "state is mandatory")
                    .IsNotNullOrEmpty(surname, "surname", "surname is mandatory")
                    .IsNotNullOrEmpty(tel, "tel", "tel is mandatory")
                    .IsLowerOrEqualsThan(expectedPaymentAmount, 0, "expectedPaymentAmount", "expectedPaymentAmount is mandatory")
                    .IsNotNullOrEmpty(contract, "contract", "contract is mandatory")
                );
        }
        private bool CheckIfIsEmpty(string value)
        {
            var teste = Guid.Parse(value) == Guid.Empty;
            return Guid.Parse(value) == Guid.Empty;
        }
    }
    public class AccountHandler : IRequestHandler<AccountCommand, ICommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _accountRepository;
        public AccountHandler(IMediator mediator, IAccountRepository accountRepository)
        {
            _mediator = mediator;
            _accountRepository = accountRepository;
        }
        public async Task<ICommandResult> Handle(AccountCommand request, CancellationToken cancellationToken)
        {
            await _accountRepository.CreateAsync(Account.AccountFactory.NewAccount(
                request.addressBody,
                request.aref,
                request.authorisedBank,
                request.authorisedCard,
                request.campaignId,
                request.dateOfBirth,
                request.email,
                request.firstName,
                request.governmentID,
                request.lenderId,
                request.middleName,
                request.monthlyFee,
                request.originatorId,
                request.postcode,
                request.regularPaymentDay,
                request.state,
                request.surname,
                request.tel,
                new Product(request.productSetupFee),
                request.expectedPaymentAmount,
                request.contract));
            await _accountRepository.SaveChangesAsync();
            await _mediator.Publish(new AccountCreatedEvent(DateTime.UtcNow,"CB"));
            return new CommandResult(true, "Account registered successfully!", null);
        }
    }
}
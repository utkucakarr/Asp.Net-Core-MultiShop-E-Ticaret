﻿using MultiShop.Message.Dtos;

namespace MultiShop.Message.Services
{
    public interface IUserMessageService
    {
        Task<List<ResultMessageDto>> GetAllMessageAsync();

        Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id);

        Task<List<ResultSendBoxMessageDto>> GetSendBoxMessageAsync(string id);

        Task CreateMessageAsync (CreateMessageDto createMessageDto);

        Task UpdateMessageAsync (UpdateMessageDto updateMessageDto);

        Task DeleteMessageAsync (int id);

        Task<GetByIdMessageDto> GetByIdMessageAsync (int id);

        Task<int> GetTotalMessageCountAsync();

        Task<int> GetTotalMessageCountByRecieverId(string id);
    }
}
